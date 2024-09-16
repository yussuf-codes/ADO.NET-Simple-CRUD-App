using System.Collections.Generic;

namespace Persistence.Utilities.SQLProvider;

public interface ISQLProvider
{
    int ExecuteCommand(string query, List<StoredProcedureArg>? args = null);
    object ExecuteQuery(string query, List<StoredProcedureArg>? args = null);
    object ExecuteScalar(string query, List<StoredProcedureArg>? args = null);
}
