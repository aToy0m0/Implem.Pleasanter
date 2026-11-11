using Implem.DefinitionAccessor;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataTypes;
using Implem.Pleasanter.Libraries.General;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Implem.Pleasanter.Models
{
    public static class PermissionValidators
    {
        /// <summary>
        /// Fixed:
        /// </summary>
        public static bool GrantsOwnServerScriptManagement(
            int userId,
            IEnumerable<PermissionModel> current,
            IEnumerable<string> proposed)
        {
            var alreadyAssigned = current.Any(permission =>
                permission.UserId == userId
                && permission.PermissionType.HasFlag(Permissions.Types.ManageServerScript));
            return !alreadyAssigned && Permissions.Get(proposed.ToList()).Any(permission =>
                permission.Name == "User"
                && permission.Id == userId
                && permission.Type.HasFlag(Permissions.Types.ManageServerScript));
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static ErrorData OnUpdating(Context context, SiteSettings ss)
        {
            if (!context.CanManagePermission(ss: ss))
            {
                return new ErrorData(type: Error.Types.HasNotPermission);
            }
            foreach (var key in context.Forms.Keys)
            {
                switch (key)
                {
                    case "InheritPermission":
                        var errorData = SiteValidators.InheritPermission(
                            context: context,
                            ss: ss);
                        if (errorData.Type != Error.Types.None) return errorData;
                        break;
                }
            }
            return new ErrorData(type: Error.Types.None);
        }
    }
}
