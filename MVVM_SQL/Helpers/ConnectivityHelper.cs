using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Networking;
using System.Threading.Tasks;
using MVVM_SQL.Services;

namespace MVVM_SQL.Helpers
{
    public static class ConnectivityHelper
    {
        public static async Task CheckAndSyncDataAsync(DatabaseService databaseService)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await databaseService.SyncToServerAsync();
            }
        }
    }
}
