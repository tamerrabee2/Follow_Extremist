-- =========================================================================================
-- سكريبت شامل وتراكمي لقاعدة البيانات مع المايجريشن (Non-Destructive Safe Migration Script)
-- الإصدار: 10-10-2026
-- النظام: برنامج المتابعة ونظام البصمة والحضور (Follow System)
-- =========================================================================================
-- المميزات والضمانات:
--   1. أمان تام بنسبة 100% على البيانات الحالية (لا يحذف أي جدول أو صف أو بيانات مسجلة نهائياً).
--   2. تشغيل تكراري آمن (Idempotent): يمكن تشغيله على أي قاعدة بيانات (جديدة، قديمة، أو محدثة).
--   3. يتضمن كافة الجداول والأعمدة والفهارس المضافة حديثاً (DeviceEnrollId, SerialNumber, OutputMode, etc.).
--   4. يوثق المايجريشن تلقائياً في جدول __EFMigrationsHistory لتوافق Entity Framework Core.
-- =========================================================================================

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

PRINT N'==============================================================================';
PRINT N'>>> بدء فحص وتحديث قاعدة بيانات برنامج المتابعة (بدون المساس بالبيانات الحالية)...';
PRINT N'==============================================================================';
GO

-- =========================================================================================
-- 1. جدول معلومات العناصر الأساسي (ElementInfo)
-- =========================================================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ElementInfo')
BEGIN
    CREATE TABLE [dbo].[ElementInfo] (
        [Id]                 INT IDENTITY(1,1) NOT NULL,
        [ElementName]        NVARCHAR(MAX)     NULL,
        [NationalId]         NVARCHAR(MAX)     NULL,
        [MotherName]         NVARCHAR(MAX)     NULL,
        [Qualification]      NVARCHAR(MAX)     NULL,
        [Job]                NVARCHAR(MAX)     NULL,
        [BirthDate]          DATETIME2(7)      NOT NULL DEFAULT '1900-01-01',
        [BirthPlace]         NVARCHAR(MAX)     NULL,
        [ElementImage]       VARBINARY(MAX)    NULL,
        [NationalIdImage]    VARBINARY(MAX)    NULL,
        [FollowState]        NVARCHAR(MAX)     NULL,
        [ReasonEndFollow]    NVARCHAR(MAX)     NULL,
        [Notes]              NVARCHAR(MAX)     NULL,
        [Phone]              NVARCHAR(MAX)     NULL,
        [Mobile]             NVARCHAR(MAX)     NULL,
        [Mobile2]            NVARCHAR(MAX)     NULL,
        [Mobile3]            NVARCHAR(MAX)     NULL,
        [DateFollowStart]    DATETIME2(7)      NOT NULL DEFAULT GETDATE(),
        [FollowDaysCount]    INT               NOT NULL DEFAULT 0,
        [DateFollowNow]      DATETIME2(7)      NOT NULL DEFAULT GETDATE(),
        [Address]            NVARCHAR(MAX)     NULL,
        [DateFollowNext]     DATETIME2(7)      NOT NULL DEFAULT GETDATE(),
        [RegulatoryStatus]   NVARCHAR(MAX)     NULL,
        [FacebookAcount]     NVARCHAR(MAX)     NULL,
        [FacebookID]         NVARCHAR(MAX)     NULL,
        [PrisonedOrnot]      NVARCHAR(MAX)     NULL,
        [CaseData]           NVARCHAR(MAX)     NULL,
        [DeviceEnrollId]     INT               NULL,
        CONSTRAINT [PK_ElementInfo] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT N'[✓] تم إنشاء جدول العناصر (ElementInfo) بنجاح.';
END
ELSE
BEGIN
    PRINT N'[-] جدول العناصر (ElementInfo) موجود مسبقاً.';
    
    -- التحقق من إضافة عمود DeviceEnrollId
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[ElementInfo]') AND name = N'DeviceEnrollId')
    BEGIN
        ALTER TABLE [dbo].[ElementInfo] ADD [DeviceEnrollId] INT NULL;
        PRINT N'[✓] تم إضافة عمود كود الماكينة الموحد (DeviceEnrollId) إلى جدول ElementInfo.';
    END
END
GO

-- إنشاء فهرس DeviceEnrollId
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ElementInfo_DeviceEnrollId' AND object_id = OBJECT_ID(N'[dbo].[ElementInfo]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ElementInfo_DeviceEnrollId] ON [dbo].[ElementInfo] ([DeviceEnrollId] ASC);
    PRINT N'[✓] تم إنشاء الفهرس (IX_ElementInfo_DeviceEnrollId).';
END
GO

-- =========================================================================================
-- 2. جدول أجهزة البصمة (FingerprintDevice)
-- =========================================================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'FingerprintDevice')
BEGIN
    CREATE TABLE [dbo].[FingerprintDevice] (
        [Id]            INT IDENTITY(1,1) NOT NULL,
        [DeviceName]    NVARCHAR(MAX)     NULL,
        [IpAddress]     NVARCHAR(MAX)     NULL,
        [Port]          INT               NOT NULL DEFAULT 4370,
        [MachineNumber] INT               NOT NULL DEFAULT 1,
        [CommPassword]  NVARCHAR(MAX)     NULL,
        [Location]      NVARCHAR(MAX)     NULL,
        [IsEnabled]     BIT               NOT NULL DEFAULT 1,
        [LastSyncTime]  DATETIME2(7)      NULL,
        [Status]        NVARCHAR(MAX)     NULL,
        [Notes]         NVARCHAR(MAX)     NULL,
        [SerialNumber]  NVARCHAR(MAX)     NULL,
        CONSTRAINT [PK_FingerprintDevice] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT N'[✓] تم إنشاء جدول أجهزة البصمة (FingerprintDevice) بنجاح.';
END
ELSE
BEGIN
    PRINT N'[-] جدول أجهزة البصمة (FingerprintDevice) موجود مسبقاً.';

    -- التحقق من إضافة عمود SerialNumber
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[FingerprintDevice]') AND name = N'SerialNumber')
    BEGIN
        ALTER TABLE [dbo].[FingerprintDevice] ADD [SerialNumber] NVARCHAR(MAX) NULL;
        PRINT N'[✓] تم إضافة عمود السيريال العتادي (SerialNumber) إلى جدول FingerprintDevice.';
    END
END
GO

-- =========================================================================================
-- 3. جدول إعدادات الطباعة والشاشة (PrintSetting)
-- =========================================================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'PrintSetting')
BEGIN
    CREATE TABLE [dbo].[PrintSetting] (
        [Id]                    INT IDENTITY(1,1) NOT NULL,
        [PrinterType]           NVARCHAR(MAX)     NULL,
        [PrinterName]           NVARCHAR(MAX)     NULL,
        [PaperWidthMm]          INT               NOT NULL DEFAULT 80,
        [AutoPrintOnAttendance] BIT               NOT NULL DEFAULT 1,
        [PrintCopies]           INT               NOT NULL DEFAULT 1,
        [HeaderText]            NVARCHAR(MAX)     NULL,
        [FooterText]            NVARCHAR(MAX)     NULL,
        [ShowBarcode]           BIT               NOT NULL DEFAULT 1,
        [ShowNationalId]        BIT               NOT NULL DEFAULT 1,
        [ShowNextFollowDate]    BIT               NOT NULL DEFAULT 1,
        [OutputMode]            NVARCHAR(MAX)     NULL DEFAULT N'Both',
        [ScreenDurationSeconds] INT               NOT NULL DEFAULT 12,
        CONSTRAINT [PK_PrintSetting] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    INSERT INTO [dbo].[PrintSetting] (
        [PrinterType], [PrinterName], [PaperWidthMm], [AutoPrintOnAttendance], 
        [PrintCopies], [HeaderText], [FooterText], [ShowBarcode], 
        [ShowNationalId], [ShowNextFollowDate], [OutputMode], [ScreenDurationSeconds]
    ) VALUES (
        N'Thermal', N'', 80, 1, 1, 
        N'حضور متابعة', N'يرجى الالتزام بموعد المتابعة القادم', 1, 1, 1, 
        N'Both', 12
    );

    PRINT N'[✓] تم إنشاء جدول إعدادات الطباعة (PrintSetting) وإدراج السجل الافتراضي.';
END
ELSE
BEGIN
    PRINT N'[-] جدول إعدادات الطباعة (PrintSetting) موجود مسبقاً.';

    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PrintSetting]') AND name = N'OutputMode')
    BEGIN
        ALTER TABLE [dbo].[PrintSetting] ADD [OutputMode] NVARCHAR(MAX) NULL DEFAULT N'Both';
        PRINT N'[✓] تم إضافة عمود OutputMode إلى جدول PrintSetting.';
    END

    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PrintSetting]') AND name = N'ScreenDurationSeconds')
    BEGIN
        ALTER TABLE [dbo].[PrintSetting] ADD [ScreenDurationSeconds] INT NOT NULL DEFAULT 12;
        PRINT N'[✓] تم إضافة عمود ScreenDurationSeconds إلى جدول PrintSetting.';
    END
END
GO

-- =========================================================================================
-- 4. جدول قوالب بصمات الأصابع (ElementFingerprint)
-- =========================================================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ElementFingerprint')
BEGIN
    CREATE TABLE [dbo].[ElementFingerprint] (
        [Id]              INT IDENTITY(1,1) NOT NULL,
        [ElementId]       INT               NOT NULL,
        [FingerIndex]     INT               NOT NULL,
        [FingerName]      NVARCHAR(MAX)     NULL,
        [TemplateData]    NVARCHAR(MAX)     NULL,
        [TemplateVersion] INT               NOT NULL DEFAULT 10,
        [CreatedDate]     DATETIME2(7)      NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_ElementFingerprint] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_ElementFingerprint_ElementInfo_ElementId] 
            FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ElementInfo] ([Id]) ON DELETE CASCADE
    );

    CREATE NONCLUSTERED INDEX [IX_ElementFingerprint_ElementId] 
        ON [dbo].[ElementFingerprint] ([ElementId] ASC);

    PRINT N'[✓] تم إنشاء جدول قوالب البصمات (ElementFingerprint) بنجاح.';
END
ELSE
BEGIN
    PRINT N'[-] جدول قوالب البصمات (ElementFingerprint) موجود مسبقاً.';
END
GO

-- =========================================================================================
-- 5. جدول سجل العناصر المطلوبة أمنياً (ElementWantedStatus)
-- =========================================================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'ElementWantedStatus')
BEGIN
    CREATE TABLE [dbo].[ElementWantedStatus] (
        [Id]           INT IDENTITY(1,1) NOT NULL,
        [ElementId]    INT               NOT NULL,
        [IsWanted]     BIT               NOT NULL DEFAULT 1,
        [WantedReason] NVARCHAR(MAX)     NULL,
        [WantedDate]   DATETIME2(7)      NULL,
        [WantedBy]     NVARCHAR(MAX)     NULL,
        [Notes]        NVARCHAR(MAX)     NULL,
        CONSTRAINT [PK_ElementWantedStatus] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_ElementWantedStatus_ElementInfo_ElementId] 
            FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ElementInfo] ([Id]) ON DELETE CASCADE
    );

    CREATE UNIQUE NONCLUSTERED INDEX [IX_ElementWantedStatus_ElementId] 
        ON [dbo].[ElementWantedStatus] ([ElementId] ASC);

    PRINT N'[✓] تم إنشاء جدول المطلوبين (ElementWantedStatus) بنجاح.';
END
ELSE
BEGIN
    PRINT N'[-] جدول المطلوبين (ElementWantedStatus) موجود مسبقاً.';
END
GO

-- =========================================================================================
-- 6. جدول حركات الحضور والانصراف (AttendanceLog)
-- =========================================================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'AttendanceLog')
BEGIN
    CREATE TABLE [dbo].[AttendanceLog] (
        [Id]                     INT IDENTITY(1,1) NOT NULL,
        [ElementId]              INT               NOT NULL,
        [DeviceId]               INT               NULL,
        [AttendanceDateTime]     DATETIME2(7)      NOT NULL DEFAULT GETDATE(),
        [VerifyType]             INT               NOT NULL DEFAULT 1,
        [IsWantedAtTime]         BIT               NOT NULL DEFAULT 0,
        [NextFollowDateAssigned] DATETIME2(7)      NULL,
        [IsPrinted]              BIT               NOT NULL DEFAULT 0,
        [PrintedDate]            DATETIME2(7)      NULL,
        [PrintType]              NVARCHAR(MAX)     NULL,
        [Status]                 NVARCHAR(MAX)     NULL,
        [Notes]                  NVARCHAR(MAX)     NULL,
        [DeviceEnrollId]         INT               NULL,
        [DeviceSerialNumber]     NVARCHAR(MAX)     NULL,
        CONSTRAINT [PK_AttendanceLog] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_AttendanceLog_ElementInfo_ElementId] 
            FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ElementInfo] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AttendanceLog_FingerprintDevice_DeviceId] 
            FOREIGN KEY ([DeviceId]) REFERENCES [dbo].[FingerprintDevice] ([Id]) ON DELETE SET NULL
    );

    CREATE NONCLUSTERED INDEX [IX_AttendanceLog_ElementId] 
        ON [dbo].[AttendanceLog] ([ElementId] ASC);

    CREATE NONCLUSTERED INDEX [IX_AttendanceLog_DeviceId] 
        ON [dbo].[AttendanceLog] ([DeviceId] ASC);

    PRINT N'[✓] تم إنشاء جدول حركات الحضور (AttendanceLog) بنجاح.';
END
ELSE
BEGIN
    PRINT N'[-] جدول حركات الحضور (AttendanceLog) موجود مسبقاً.';

    -- إضافة عمود DeviceEnrollId إن لم يكن موجوداً
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[AttendanceLog]') AND name = N'DeviceEnrollId')
    BEGIN
        ALTER TABLE [dbo].[AttendanceLog] ADD [DeviceEnrollId] INT NULL;
        PRINT N'[✓] تم إضافة عمود كود الماكينة (DeviceEnrollId) إلى جدول AttendanceLog.';
    END

    -- إضافة عمود DeviceSerialNumber إن لم يكن موجوداً
    IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[AttendanceLog]') AND name = N'DeviceSerialNumber')
    BEGIN
        ALTER TABLE [dbo].[AttendanceLog] ADD [DeviceSerialNumber] NVARCHAR(MAX) NULL;
        PRINT N'[✓] تم إضافة عمود سيريال الماكينة (DeviceSerialNumber) إلى جدول AttendanceLog.';
    END
END
GO

-- إنشاء فهارس AttendanceLog
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_AttendanceLog_DeviceEnrollId' AND object_id = OBJECT_ID(N'[dbo].[AttendanceLog]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_AttendanceLog_DeviceEnrollId] ON [dbo].[AttendanceLog] ([DeviceEnrollId] ASC);
    PRINT N'[✓] تم إنشاء الفهرس (IX_AttendanceLog_DeviceEnrollId).';
END
GO

-- =========================================================================================
-- 7. فيو حضور اليوم اللحظي (vw_TodayAttendance)
-- =========================================================================================
IF OBJECT_ID(N'dbo.vw_TodayAttendance', N'V') IS NOT NULL
BEGIN
    DROP VIEW [dbo].[vw_TodayAttendance];
END
GO

CREATE VIEW [dbo].[vw_TodayAttendance] AS
SELECT 
    a.[Id] AS [AttendanceLogId],
    a.[ElementId],
    e.[ElementName],
    e.[NationalId],
    ISNULL(a.[DeviceEnrollId], e.[DeviceEnrollId]) AS [DeviceEnrollId],
    a.[AttendanceDateTime],
    ISNULL(d.[DeviceName], N'يدوي / غير محدد') AS [DeviceName],
    ISNULL(a.[DeviceSerialNumber], d.[SerialNumber]) AS [DeviceSerialNumber],
    ISNULL(w.[IsWanted], 0) AS [IsWanted],
    a.[NextFollowDateAssigned] AS [NextFollowDate],
    a.[IsPrinted],
    a.[Status]
FROM [dbo].[AttendanceLog] a
INNER JOIN [dbo].[ElementInfo] e ON a.[ElementId] = e.[Id]
LEFT JOIN [dbo].[FingerprintDevice] d ON a.[DeviceId] = d.[Id]
LEFT JOIN [dbo].[ElementWantedStatus] w ON a.[ElementId] = w.[ElementId]
WHERE CAST(a.[AttendanceDateTime] AS DATE) = CAST(GETDATE() AS DATE);
GO

PRINT N'[✓] تم إنشاء/تحديث الفيو (vw_TodayAttendance) بنجاح.';
GO

-- =========================================================================================
-- 8. توثيق تاريخ الميجريشن في جدول __EFMigrationsHistory
-- =========================================================================================
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'__EFMigrationsHistory')
BEGIN
    CREATE TABLE [dbo].[__EFMigrationsHistory] (
        [MigrationId]    NVARCHAR(150) NOT NULL,
        [ProductVersion] NVARCHAR(32)  NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
    );
    PRINT N'[✓] تم إنشاء جدول تاريخ الميجريشن (__EFMigrationsHistory).';
END
GO

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'__EFMigrationsHistory')
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20261008164818_AddFingerprintAttendanceSystem')
    BEGIN
        INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
        VALUES (N'20261008164818_AddFingerprintAttendanceSystem', N'9.0.0');
        PRINT N'[✓] تم توثيق ميجريشن AddFingerprintAttendanceSystem.';
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20261008221500_AddOutputModeToPrintSetting')
    BEGIN
        INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
        VALUES (N'20261008221500_AddOutputModeToPrintSetting', N'9.0.0');
        PRINT N'[✓] تم توثيق ميجريشن AddOutputModeToPrintSetting.';
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20261010103715_AddDeviceEnrollIdAndHardwareFields')
    BEGIN
        INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
        VALUES (N'20261010103715_AddDeviceEnrollIdAndHardwareFields', N'9.0.0');
        PRINT N'[✓] تم توثيق ميجريشن AddDeviceEnrollIdAndHardwareFields.';
    END
END
GO

PRINT N'==============================================================================';
PRINT N'>>> اكتمل فحص وتحديث قاعدة البيانات بنجاح تام، وكافة البيانات الأصلية محفوظة 100%.';
PRINT N'==============================================================================';
GO
