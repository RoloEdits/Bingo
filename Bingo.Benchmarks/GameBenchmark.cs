﻿using BenchmarkDotNet.Attributes;
using Bingo.Library;

namespace Bingo.Benchmarks;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class GameBenchmark
{
    // 4x3
    private const string Key4x3 = "YYYYYYNYYYYY";
    private static readonly Player Player4x3 = new Player("Rolo", Key4x3);
    private static readonly Card Format4x3 = new Card(4, 3, 10, 20, 1, 2);
    private static readonly List<Player> Players4x3 = new List<Player> { Player4x3 };

    private static readonly Game Game4x3 = new Game(Players4x3, Format4x3, Key4x3);

    // 7x5
    private const string Key7x5 = "YYYYYYYYYYYYYYYNYYYYYYYYYYYYYYYYYYY";
    private static readonly Player Player7x5 = new Player("Rolo", Key7x5);
    private static readonly Card Format7x5 = new Card(7, 5, 10, 20, 2, 2);
    private static readonly List<Player> Players7x5 = new List<Player> { Player7x5 };

    private static readonly Game Game7x5 = new Game(Players7x5, Format7x5, Key4x3);

    // 10x10
    private const string Key10x10 =
        "NYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";

    private static readonly Player Player10x10 = new Player("Rolo", Key10x10);
    private static readonly Card Format10x10 = new Card(10, 10, 10, 20, 3, 2);
    private static readonly List<Player> Players10x10 = new List<Player> { Player10x10 };
    private static readonly Game Game10x10 = new Game(Players10x10, Format10x10, Key10x10);

    [Benchmark]
    public void CalculateScoreBenchmarkFor4x3()
    {
        Game.CalculatePlayerScore(Key4x3, Key4x3, false, Game4x3);
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor7x5()
    {
        Game.CalculatePlayerScore(Key7x5, Key7x5, false, Game7x5);
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor10x10()
    {
        Game.CalculatePlayerScore(Key10x10, Key10x10, false, Game10x10);
    }
}
