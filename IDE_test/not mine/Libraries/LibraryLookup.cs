using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IDE_test;
using Newtonsoft.Json;

namespace AtlusScriptLibrary.Common.Libraries
{
    public static class LibraryLookup
    {
        internal static readonly string LibraryBaseDirectoryPath = FilePaths.libraryPath;//Path.Combine( Path.GetDirectoryName(FilePaths.compilerFilePath), "Libraries" );
        private static readonly List<Library> sLibraries;
        private static readonly Dictionary< string, Library > sLibrariesByShortName;
        private static readonly Dictionary<string, Library> sLibrariesByFullName;

        public static IEnumerable<Library> Libraries
            => sLibraries;

        static LibraryLookup()
        {
            sLibraries = new List< Library >();

            foreach ( var path in Directory.EnumerateFiles( LibraryBaseDirectoryPath, "*.json" ) )
            {
                var library = ParseLibrary( path );
                //MessageBox.Show(path.ToString());
                sLibraries.Add( library );
            }

            sLibrariesByShortName = Libraries.ToDictionary( x => x.ShortName, StringComparer.InvariantCultureIgnoreCase );
            sLibrariesByFullName = Libraries.ToDictionary( x => x.Name, StringComparer.InvariantCultureIgnoreCase );
        }

        public static Library GetLibrary( string name )
        {
            if ( sLibrariesByShortName.TryGetValue( name, out var value ) )
                return value;

            if ( sLibrariesByFullName.TryGetValue( name, out value ) )
                return value;

            return null;
        }

        private static Library ParseLibrary( string path )
        {
            string jsonText = File.ReadAllText( path );
            return JsonConvert.DeserializeObject< Library >( jsonText );
        }
    }
}
