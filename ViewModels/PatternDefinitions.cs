namespace RiffMasterUltra.ViewModels;

using System.Collections.Generic;

public static class PatternDefinitions
{
    public static readonly Dictionary<int, List<int[]>> PatternTemplates = new()
    {
        {
            2, new List<int[]>
            {
                new[] { 1, 2 },
                new[] { 3, 4 },
                new[] { 5, 6 },
                new[] { 7, 8 },
                new[] { 9, 10 },
                new[] { 11, 0 },
                
                new [] { 0, 6 },
                new [] { 1, 7 },
                new [] { 2, 8 },
                new [] { 3, 9 },
                new [] { 4, 10 },
                new [] { 5, 11 }
            }
        },
        {
            3, new List<int[]>
            {
                new[] { 0, 4, 8 },  
                new[] { 1, 5, 9 }, 
                new[] { 2, 6, 10 },  
                new[] { 3, 7, 11 }, 
                
                new[] { 0, 2, 4 }, 
                new[] { 1, 3, 5 }, 
                new[] { 6, 8, 10 }, 
                new[] { 7, 9, 11 } 
            }
        },
        {
            4, new List<int[]>
            {
                new[] { 0, 3, 6, 9 },  
                new[] { 1, 4, 7, 10 }, 
                new[] { 2, 5, 8, 11 },
                
                new[] { 0, 1, 6, 7 },
                new[] { 2, 3, 8, 9 },
                new[] { 4, 5, 10, 11 },
            }
        },
        {
            6, new List<int[]>
            {
                new[] { 0, 2, 4, 6, 8, 10 },
                new[] { 1, 3, 5, 7, 9, 11 }
            }
        },
    };
}