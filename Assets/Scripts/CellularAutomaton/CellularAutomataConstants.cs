using System.Collections.Generic;
using SimpleFileBrowser;
using UnityEngine;

namespace CellularAutomaton
{
    public static class CellularAutomataConstants
    {
        public static readonly Dictionary<CellType, Color> CellColors = new Dictionary<CellType, Color>
        {
            { CellType.Floor, Color.white },
            { CellType.Wall, Color.black },
            { CellType.Start, Color.green },
            { CellType.End, Color.red }
        };

        public static readonly string InitialExportFilename = "ca.json";
        public static readonly string DefaultExportFileFilter = ".json";
        public static readonly string ExportDialogTitle = "Choose file location for export";
        public static readonly string ExportButtonText = "Export";
        public static readonly string ImportButtonText = "Import";
        public static readonly string ImportDialogTitle = "Import Cellular Automaton";
        
        
        public static readonly FileBrowser.Filter ExportFileFilter = new FileBrowser.Filter("Automaton File", ".json");
    }
}