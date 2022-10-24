namespace RiffMasterUltra.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

public class NoteSetViewModel
{
    private readonly object collectionLock = new();
    private static readonly Random Rng = new();

    public ObservableCollection<Note> Notes { get; set; } = new();

    public ObservableCollection<List<Note>> Groupings { get; set; } = new();
    
    public ObservableCollection<List<Note>> Patterns { get; set; } = new();

    public Prop<int> GroupSize { get; set; } = new() { Value = 3 };

    public CollectionView AvailableGroupSizes { get; set; } = new ListCollectionView(Enumerable.Range(2, 11).Select(x => x).ToList());

    public NoteSetViewModel()
    {
        Application.Current.Dispatcher.BeginInvoke(() => BindingOperations.EnableCollectionSynchronization(this.Notes, this.collectionLock));

        this.GenerateNoteSet();
    }

    public void GenerateNoteSet()
    {
        var notes = Enum.GetValues<NoteNames>();
        Shuffle(notes);

        this.Notes.Clear();

        foreach (var note in notes)
        {
            this.Notes.Add(new Note { Name = note });
        }
    }

    public void GenerateGroupings()
    {
        this.Groupings.Clear();

        var groupings = new List<List<Note>>();

        var size = this.GroupSize.Value;
        
        //special cases
        if (size == 12)
        {
            groupings.Add(new List<Note>(this.Notes));
            groupings.Add(new List<Note>(this.Notes.Reverse()));
        }
        else
        {
            var qty = (int)Math.Ceiling(12d / size);
            for (var i = 0; i < qty; i++)
            {
                groupings.Add(this.GetGrouping((i * size) % 12, size));
            }
        }

        foreach (var grouping in groupings)
        {
            this.Groupings.Add(grouping);
        }
    }

    public void GeneratePatterns()
    {
        this.Patterns.Clear();

        var size = this.GroupSize.Value;
        if (!PatternDefinitions.PatternTemplates.ContainsKey(size))
        {
            return;
        }

        var templates = PatternDefinitions.PatternTemplates[size];
        
        var patterns = templates.Select(template => template.Select(x => this.Notes[x]).ToList()).ToList();

        foreach (var pattern in patterns)
        {
            this.Patterns.Add(pattern);
        }
    }

    private List<Note> GetGrouping(int start, int size)
    {
        var result = new List<Note>();
        
        for (var i = 0; i < size; i++)
        {
            result.Add(this.Notes[(start + i) % 12]);
        }
        
        return result;
    }

    private static void Shuffle<T>(IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = Rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}