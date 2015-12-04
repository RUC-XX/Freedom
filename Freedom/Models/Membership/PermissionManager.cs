using System;
using System.Xml;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;
using System.Linq;
using WebMatrix.WebData;

namespace Freedom
{
    public class PermissionManager
    {
        public static bool CheckUserHasPermision(string userName, string permissionName)
        {
            List<Role> roleList = new List<Role>();
            List<PermissionsInRoles> permissionsInRolesList = new List<PermissionsInRoles>();
            if (HttpRuntime.Cache.Get("Roles") == null)
            {
                using (FreedomMembershipContext db = new FreedomMembershipContext())
                {
                    roleList = db.Roles.AsEnumerable<Role>().ToList<Role>();

                    HttpRuntime.Cache.Insert("Roles", roleList);
                }
            }

            if (HttpRuntime.Cache.Get("PermissionsInRoles") == null)
            {
                using (FreedomMembershipContext db = new FreedomMembershipContext())
                {
                    permissionsInRolesList = db.PermissionsInRoles
                                                .Include("Permission").Include("Role")
                                                .AsEnumerable<PermissionsInRoles>().ToList<PermissionsInRoles>();
                    HttpRuntime.Cache.Insert("PermissionsInRoles", permissionsInRolesList);
                }
            }

            string[] currentRoles = new string[] { };
            if (HttpRuntime.Cache.Get("CurrentRoles") == null)
            {
                
                currentRoles = Roles.GetRolesForUser(userName);
                HttpRuntime.Cache.Insert("CurrentRoles", currentRoles);
            }

            currentRoles = HttpRuntime.Cache.Get("CurrentRoles") as string[];
            roleList = HttpRuntime.Cache.Get("Roles") as List<Role>;
            permissionsInRolesList = HttpRuntime.Cache.Get("PermissionsInRoles") as List<PermissionsInRoles>;

            


            foreach (var roleName in currentRoles)
            {
                List<Permission> permissionList = permissionsInRolesList.Where(e => e.Role.RoleName == roleName)
                                                                            .Select(e => e.Permission).ToList<Permission>();

                foreach (var permission in permissionList)
                {
                    if (permission.PermissionName == permissionName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
