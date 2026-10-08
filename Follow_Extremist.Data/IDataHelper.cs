using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Extremist.Data
{
    
    public interface IDataHelper<Table>
    {
        // Read

        /// <summary>Gets all data.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        List<Table> GetAllData();

        /// <summary>Searches the specified search item.</summary>
        /// <param name="SearchItem">The search item.</param>
        /// <returns>Searched Data</returns>
        List<Table> Search(String SearchItem);
        /// <summary>Finds the specified identifier.</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Row Table when id == id</returns>
        Table Find(int Id);

        // Write
        /// <summary>Adds the specified table.</summary>
        /// <param name="table">The table.</param>
        /// <returns>1 when the task success 0 when task faild</returns>
        int Add(Table table);
        /// <summary>Edits the specified table.</summary>
        /// <param name="table">The table.</param>
        /// <returns>1 when the task success 0 when task faild</returns>
        int Edit(Table table);
        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>1 when the task success 0 when task faild</returns>
        int Delete(int Id);

        // Read Async
        /// <summary>Gets all data asynchronous.</summary>
        /// <returns>All Data of Table</returns>
        Task<List<Table>> GetAllDataAsync();
        /// <summary>Searches the asynchronous.</summary>
        /// <param name="SearchItem">The search item.</param>
        /// <returns>Searched Data <see langword="async"/></returns>
        Task<List<Table>> SearchAsync(String SearchItem);
        /// <summary>Finds the asynchronous.</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Row Table when id == id</returns>
        Task<Table> FindAsync(int Id);

        // Write Async
        /// <summary>Adds the asynchronous.</summary>
        /// <param name="table">The table.</param>
        /// <returns>1 when the task success 0 when task faild</returns>
        Task<int> AddAsync(Table table);
        /// <summary>Edits the asynchronous.</summary>
        /// <param name="table">The table.</param>
        /// <returns>1 when the task success 0 when task faild</returns>
        Task<int> EditAsync(Table table);
        /// <summary>Deletes the asynchronous.</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>1 when the task success 0 when task faild</returns>
        Task<int> DeleteAsync(int Id);

        Task<List<Table>> GetFilteredDataAsync();
    }
}
