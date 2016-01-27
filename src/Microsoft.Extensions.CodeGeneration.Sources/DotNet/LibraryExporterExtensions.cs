using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.DotNet.ProjectModel.Compilation;
using Microsoft.DotNet.ProjectModel;
using Microsoft.DotNet.ProjectModel.Graph;
using System.IO;
using System.Reflection;

namespace Microsoft.Extensions.CodeGeneration.DotNet
{
    public static class LibraryExporterExtensions
    {
        public static string GetResolvedPathForDependency(this ILibraryExporter _libraryExporter, LibraryDescription library)
        {
            if(library == null) 
            {
                throw new ArgumentNullException(nameof(library));
            }
            
            var exports = _libraryExporter.GetAllExports();
            var assets = exports.SelectMany(_ => _.RuntimeAssemblies)
                                    .Where(_ => _.Name == library.Identity.Name);
            if(assets.Any())  
            {
                return assets.First().ResolvedPath;
            }
            assets = exports.SelectMany(_ => _.NativeLibraries)
                                .Where(_ => _.Name == library.Identity.Name);
            if(assets.Any())  
            {
                return assets.First().ResolvedPath;
            }
            assets = exports.SelectMany(_ => _.CompilationAssemblies)
                                .Where(_ => _.Name == library.Identity.Name);
            if(assets.Any()) 
            {
                return assets.First().ResolvedPath;
            }
            return string.Empty;
        }
    }
}