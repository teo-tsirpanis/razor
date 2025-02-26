﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Microsoft.AspNetCore.Razor.Language.Legacy;

internal partial class ParserContext
{
    public ParserContext(RazorSourceDocument source, RazorParserOptions options)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        SourceDocument = source;

        Source = new SeekableTextReader(SourceDocument);
        DesignTimeMode = options.DesignTime;
        FeatureFlags = options.FeatureFlags;
        ParseLeadingDirectives = options.ParseLeadingDirectives;
        ErrorSink = new ErrorSink();
        SeenDirectives = new HashSet<string>(StringComparer.Ordinal);
    }

    public ErrorSink ErrorSink { get; set; }

    public RazorParserFeatureFlags FeatureFlags { get; }

    public HashSet<string> SeenDirectives { get; }

    public ITextDocument Source { get; }

    public RazorSourceDocument SourceDocument { get; }

    public bool DesignTimeMode { get; }

    public bool ParseLeadingDirectives { get; }

    public bool WhiteSpaceIsSignificantToAncestorBlock { get; set; }

    public bool NullGenerateWhitespaceAndNewLine { get; set; }

    public bool InTemplateContext { get; set; }

    public bool StartOfLine { get; set; } = true;

    public AcceptedCharactersInternal LastAcceptedCharacters { get; set; } = AcceptedCharactersInternal.None;

    public bool EndOfFile
    {
        get { return Source.Peek() == -1; }
    }
}

// Debug Helpers

#if DEBUG
[DebuggerDisplay("{" + nameof(DebuggerToString) + "(),nq}")]
internal partial class ParserContext
{
    private const int InfiniteLoopCountThreshold = 1000;
    private int _infiniteLoopGuardCount;
    private SourceLocation? _infiniteLoopGuardLocation;

    internal string Unparsed
    {
        get
        {
            var remaining = ((TextReader)Source).ReadToEnd();
            Source.Position -= remaining.Length;
            return remaining;
        }
    }

    private bool CheckInfiniteLoop()
    {
        // Infinite loop guard
        //  Basically, if this property is accessed 1000 times in a row without having advanced the source reader to the next position, we
        //  cause a parser error
        if (_infiniteLoopGuardLocation != null)
        {
            if (Source.Location.Equals(_infiniteLoopGuardLocation.Value))
            {
                _infiniteLoopGuardCount++;
                if (_infiniteLoopGuardCount > InfiniteLoopCountThreshold)
                {
                    Debug.Fail("An internal parser error is causing an infinite loop at this location.");

                    return true;
                }
            }
            else
            {
                _infiniteLoopGuardCount = 0;
            }
        }
        _infiniteLoopGuardLocation = Source.Location;
        return false;
    }

    private string DebuggerToString()
    {
        return Unparsed;
    }
}
#endif
