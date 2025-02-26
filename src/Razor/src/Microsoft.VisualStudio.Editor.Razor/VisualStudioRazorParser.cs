﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.VisualStudio.Text;

namespace Microsoft.VisualStudio.Editor.Razor;

public abstract class VisualStudioRazorParser
{
    public abstract event EventHandler<DocumentStructureChangedEventArgs> DocumentStructureChanged;

    public abstract string FilePath { get; }

    public abstract RazorCodeDocument? CodeDocument { get; }

    public abstract ITextSnapshot? Snapshot { get; }

    public abstract ITextBuffer TextBuffer { get; }

    public abstract bool HasPendingChanges { get; }

    public abstract void QueueReparse();

    internal virtual Task<RazorCodeDocument?> GetLatestCodeDocumentAsync(ITextSnapshot atOrNewerSnapshot, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
