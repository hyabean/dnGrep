﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using dnGREP.Common;

namespace dnGREP.WPF
{
    public partial class MainViewModel
    {
        public static readonly IDictionary<string, IScriptCommand> SetCommandMap = new Dictionary<string, IScriptCommand>();
        public static readonly IDictionary<string, IScriptCommand> UseCommandMap = new Dictionary<string, IScriptCommand>();
        public static readonly IDictionary<string, IScriptCommand> AddCommandMap = new Dictionary<string, IScriptCommand>();
        public static readonly IDictionary<string, IScriptCommand> RemoveCommandMap = new Dictionary<string, IScriptCommand>();
        public static readonly IDictionary<string, IScriptCommand> ReportCommandMap = new Dictionary<string, IScriptCommand>();

        private bool cancelingScript = false;

        public void InitializeScriptTargets()
        {
            if (SetCommandMap.Count == 0)
            {
                SetCommandMap.Add("folder", new ScriptCommand<string>(p => FileOrFolderPath = Utils.QuoteIfNeeded(p)));
                SetCommandMap.Add("pathtomatch", new ScriptCommand<string>(p => FilePattern = p) { AllowNullValue = true });
                SetCommandMap.Add("pathtoignore", new ScriptCommand<string>(p => FilePatternIgnore = p) { AllowNullValue = true });
                SetCommandMap.Add("searchinarchives", new ScriptCommand<bool>(p => IncludeArchive = p));

                SetCommandMap.Add("patterntype", new ScriptCommand<FileSearchType>(p => TypeOfFileSearch = p));
                SetCommandMap.Add("searchparallel", new ScriptCommand<bool>(p => SearchParallel = p));
                SetCommandMap.Add("usegitignore", new ScriptCommand<bool>(p => UseGitignore = p));
                SetCommandMap.Add("skipremotecloudstoragefiles", new ScriptCommand<bool>(p => SkipRemoteCloudStorageFiles = p));
                SetCommandMap.Add("encoding", new ScriptCommand<int>(p => CodePage = p));

                SetCommandMap.Add("filesizefilter", new ScriptCommand<FileSizeFilter>(p => UseFileSizeFilter = p));
                SetCommandMap.Add("sizefrom", new ScriptCommand<int>(p => SizeFrom = p));
                SetCommandMap.Add("sizeto", new ScriptCommand<int>(p => SizeTo = p));
                SetCommandMap.Add("includesubfolder", new ScriptCommand<bool>(p => IncludeSubfolder = p));
                SetCommandMap.Add("maxsubfolderdepth", new ScriptCommand<int>(p => MaxSubfolderDepth = p));
                SetCommandMap.Add("includehidden", new ScriptCommand<bool>(p => IncludeHidden = p));
                SetCommandMap.Add("includebinary", new ScriptCommand<bool>(p => IncludeBinary = p));
                SetCommandMap.Add("followsymlinks", new ScriptCommand<bool>(p => FollowSymlinks = p));

                SetCommandMap.Add("filedatefilter", new ScriptCommand<FileDateFilter>(p => UseFileDateFilter = p));
                SetCommandMap.Add("filetimerange", new ScriptCommand<FileTimeRange>(p => TypeOfTimeRangeFilter = p));
                SetCommandMap.Add("startdate", new ScriptCommand<DateTime>(p => StartDate = p));
                SetCommandMap.Add("enddate", new ScriptCommand<DateTime>(p => EndDate = p));
                SetCommandMap.Add("hoursfrom", new ScriptCommand<int>(p => HoursFrom = p));
                SetCommandMap.Add("hoursto", new ScriptCommand<int>(p => HoursTo = p));

                SetCommandMap.Add("searchtype", new ScriptCommand<SearchType>(p => TypeOfSearch = p));
                SetCommandMap.Add("searchfor", new ScriptCommand<string>(p => SearchFor = p) { AllowNullValue = true });
                SetCommandMap.Add("replacewith", new ScriptCommand<string>(p => ReplaceWith = p) { AllowNullValue = true });

                SetCommandMap.Add("casesensitive", new ScriptCommand<bool>(p => CaseSensitive = p));
                SetCommandMap.Add("wholeword", new ScriptCommand<bool>(p => WholeWord = p));
                SetCommandMap.Add("multiline", new ScriptCommand<bool>(p => Multiline = p));
                SetCommandMap.Add("dotasnewline", new ScriptCommand<bool>(p => Singleline = p));
                SetCommandMap.Add("booleanoperators", new ScriptCommand<bool>(p => BooleanOperators = p));
                SetCommandMap.Add("capturegroupsearch", new ScriptCommand<bool>(p => CaptureGroupSearch = p));

                SetCommandMap.Add("searchinresults", new ScriptCommand<bool>(p => SearchInResultsContent = p));
                SetCommandMap.Add("previewfile", new ScriptCommand<bool>(p => PreviewFileContent = p));
                SetCommandMap.Add("stopafterfirstmatch", new ScriptCommand<bool>(p => StopAfterFirstMatch = p));

                SetCommandMap.Add("highlightmatches", new ScriptCommand<bool>(p => HighlightsOn = p));
                SetCommandMap.Add("highlightgroups", new ScriptCommand<bool>(p => HighlightCaptureGroups = p));
                SetCommandMap.Add("showcontextlines", new ScriptCommand<bool>(p => ShowLinesInContext = p));
                SetCommandMap.Add("contextlinesbefore", new ScriptCommand<int>(p => ContextLinesBefore = p));
                SetCommandMap.Add("contextlinesafter", new ScriptCommand<int>(p => ContextLinesAfter = p));

                SetCommandMap.Add("wraptext", new ScriptCommand<bool>(p => SearchResults.WrapText = p));
                SetCommandMap.Add("resultszoom", new ScriptCommand<double>(p => SearchResults.ResultsScale = p));
                SetCommandMap.Add("sorttype", new ScriptCommand<SortType>(p => SortType = p));
                SetCommandMap.Add("sortdirection", new ScriptCommand<ListSortDirection>(p => SortDirection = p));

                SetCommandMap.Add("reportmode", new ScriptCommand<ReportMode>(p => GrepSettings.Instance.Set(GrepSettings.Key.ReportMode, p)));
                SetCommandMap.Add("fileinformation", new ScriptCommand<bool>(p => GrepSettings.Instance.Set(GrepSettings.Key.IncludeFileInformation, p)));
                SetCommandMap.Add("trimwhitespace", new ScriptCommand<bool>(p => GrepSettings.Instance.Set(GrepSettings.Key.TrimWhitespace, p)));
                SetCommandMap.Add("uniquevalues", new ScriptCommand<bool>(p => GrepSettings.Instance.Set(GrepSettings.Key.FilterUniqueValues, p)));
                SetCommandMap.Add("uniquescope", new ScriptCommand<UniqueScope>(p => GrepSettings.Instance.Set(GrepSettings.Key.UniqueScope, p)));
                SetCommandMap.Add("separatelines", new ScriptCommand<bool>(p => GrepSettings.Instance.Set(GrepSettings.Key.OutputOnSeparateLines, p)));
                SetCommandMap.Add("listitemseparator", new ScriptCommand<string>(p => GrepSettings.Instance.Set(GrepSettings.Key.ListItemSeparator, p)) { AllowNullValue = true });

                UseCommandMap.Add("bookmark", new ScriptCommand<string>(p => UseBookmark(p)));

                AddCommandMap.Add("bookmark", new ScriptCommand<string>(p => AddBookmark(p, false)) { AllowNullValue = true });
                AddCommandMap.Add("folderbookmark", new ScriptCommand<string>(p => AddBookmark(p, true)) { AllowNullValue = true });

                RemoveCommandMap.Add("bookmark", new ScriptCommand<string>(p => RemoveBookmark(p, false)) { AllowNullValue = true });
                RemoveCommandMap.Add("folderbookmark", new ScriptCommand<string>(p => RemoveBookmark(p, true)) { AllowNullValue = true });

                ReportCommandMap.Add("full", new ScriptCommand<string>(p =>
                    ReportWriter.SaveResultsReport(SearchResults.GetList(), BooleanOperators, SearchFor, GetSearchOptions(), p)));
                ReportCommandMap.Add("text", new ScriptCommand<string>(p =>
                    ReportWriter.SaveResultsAsText(SearchResults.GetList(), SearchResults.TypeOfSearch, p)));
                ReportCommandMap.Add("csv", new ScriptCommand<string>(p =>
                    ReportWriter.SaveResultsAsCSV(SearchResults.GetList(), SearchResults.TypeOfSearch, p)));
            }
        }

        public IScriptCommand ShowFileOptionsCommand => new ScriptCommand<bool>(
            param => IsFiltersExpanded = param);

        public IScriptCommand MaximizeResultsCommand => new ScriptCommand<bool>(
            param => IsResultTreeMaximized = param);

        public IScriptCommand DeleteFilesScriptCommand => new ScriptCommand<bool>(
            param => DeleteFiles(param));

        private void PopulateScripts()
        {
            InitializeScriptTargets();

            ScriptMenuItems.Add(new MenuItemViewModel("New Script…", new RelayCommand(p => NewScript(), q => !IsScriptRunning)));
            ScriptMenuItems.Add(new MenuItemViewModel("Edit Script…", new RelayCommand(p => EditScript(), q => !IsScriptRunning)));
            ScriptMenuItems.Add(new MenuItemViewModel("Cancel Script", new RelayCommand(p => CancelScript(), q => IsScriptRunning)));
            ScriptMenuItems.Add(new MenuItemViewModel(null, null));
            foreach (var name in ScriptManager.Instance.Scripts)
            {
                ScriptMenuItems.Add(new MenuItemViewModel(name,
                    new RelayCommand(p => RunScript(name), q => !IsScriptRunning)));
            }
        }


        private bool isScriptRunning = false;
        public bool IsScriptRunning
        {
            get { return isScriptRunning; }
            set
            {
                if (isScriptRunning == value)
                {
                    return;
                }

                isScriptRunning = value;
                OnPropertyChanged(nameof(IsScriptRunning));
            }
        }

        private void NewScript()
        {
        }

        private void EditScript()
        {
        }

        private void CancelScript()
        {
            cancelingScript = true;
            Cancel();
        }

        private Queue<ScriptStatement> currentScript = null;

        private void RunScript(string name)
        {
            if (currentScript == null)
            {
                currentScript = ScriptManager.Instance.ParseScript(name);
                CommandManager.InvalidateRequerySuggested();
                cancelingScript = false;
                IsScriptRunning = true;
                ContinueScript();
            }
        }

        private void ContinueScript()
        {
            while (currentScript != null && currentScript.Count > 0)
            {
                if (cancelingScript || Utils.CancelSearch)
                {
                    currentScript.Clear();
                    break;
                }

                var stmt = currentScript.Dequeue();
                switch (stmt.Command)
                {
                    case "set":
                        if (SetCommandMap.TryGetValue(stmt.Target, out IScriptCommand set))
                        {
                            set.Execute(stmt.Value);
                        }
                        break;

                    case "use":
                        if (UseCommandMap.TryGetValue(stmt.Target, out IScriptCommand use))
                        {
                            use.Execute(stmt.Value);
                        }
                        break;

                    case "add":
                        if (AddCommandMap.TryGetValue(stmt.Target, out IScriptCommand add))
                        {
                            add.Execute(stmt.Value);
                        }
                        break;

                    case "remove":
                        if (RemoveCommandMap.TryGetValue(stmt.Target, out IScriptCommand remove))
                        {
                            remove.Execute(stmt.Value);
                        }
                        break;

                    case "report":
                        if (ReportCommandMap.TryGetValue(stmt.Target, out IScriptCommand rpt))
                        {
                            rpt.Execute(stmt.Value);
                        }
                        break;

                    case "reset":
                        ResetOptionsCommand.Execute(null);
                        break;

                    case "sort":
                        SortCommand.Execute(null);
                        break;

                    case "undo":
                        UndoCommand.Execute(null);
                        break;

                    case "copyfiles":
                        CopyFilesCommand.Execute(stmt.Value);
                        break;

                    case "movefiles":
                        MoveFilesCommand.Execute(stmt.Value);
                        break;

                    case "deletefiles":
                        DeleteFilesScriptCommand.Execute("false");
                        break;

                    case "copyfilenames":
                        CopyToClipboardCommand.Execute(null);
                        break;

                    case "copyresults":
                        CopyMatchingLinesCommand.Execute(null);
                        break;

                    case "showfileoptions":
                        ShowFileOptionsCommand.Execute(stmt.Value);
                        break;

                    case "maximizeresults":
                        MaximizeResultsCommand.Execute(stmt.Value);
                        break;

                    case "search":
                        SearchCommand.Execute(null);
                        return;

                    case "replace":
                        ReplaceCommand.Execute(null);
                        return;

                    case "exit":
                        Application.Current.MainWindow.Close();
                        return;
                }
            }

            if (currentScript != null && currentScript.Count == 0)
            {
                currentScript = null;
                IsScriptRunning = false;
            }

            CommandManager.InvalidateRequerySuggested();
        }
    }
}
