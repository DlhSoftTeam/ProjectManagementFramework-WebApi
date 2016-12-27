using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectManagementServices
{
    public class BaseInput
    {
        /// <summary>
        /// Input project data.
        /// </summary>
        public string ProjectXml { get; set; }
    }

    public class BaseOutput
    {
        /// <summary>
        /// Output project data.
        /// </summary>
        public string ProjectXml { get; set; }
    }
}