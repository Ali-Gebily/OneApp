using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models
{
    /// <summary>
    /// CSS Rule scope used for filteration of rules while loading app
    /// </summary>
    public enum RuleScope
    {
        /// <summary>
        /// the rule affects style of the whole app. it will be loaded on app launch
        /// </summary>
        Global = 0, 
        /// <summary>
        /// the rule affects style of the tenant . it will be loaded after tenant identified
        ///  Multi-tenant will be implemented later
        /// </summary>
        Tenant = 1,
        /// <summary>
        ///the rule  affects style of the user. it will be loaded after authentication
        /// </summary>
        User = 2,
        /// <summary>
        ///the rule  affects style of the certain entity with certain Id. it will be loaded when opening a screen of this entity and this Id
        /// </summary>
        Entity = 3
    }
}