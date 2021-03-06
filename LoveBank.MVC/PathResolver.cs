﻿using System.Web.Hosting;

namespace LoveBank.MVC
{
    public class PathResolver : IPathResolver
    {
        /// <summary>
        /// Returns the physical path for the specified virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        public string Resolve(string virtualPath)
        {
            return HostingEnvironment.MapPath(virtualPath);
        }
    }
}
