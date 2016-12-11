using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models
{
    /// <summary>
    /// CSS Rule scope used for filteration of rules while loading app
    /// </summary>
    public enum RuleEntityScope
    {
        /// <summary>
        /// rule style Not specific to any entity
        /// </summary>
        Global = 0,
        /// <summary>
        /// style can be customized for specific user
        /// </summary>
        User = 1
    }
}