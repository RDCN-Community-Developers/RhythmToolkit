using RhythmBase.Events;
using RhythmBase.LevelElements;
using RhythmBase.Components;
using Physicians;
using static RhythmBase.Extensions.EventsExtension;

//level.SaveFile(new FileInfo("E:\\Resources\\你我皆相连2\\Connected\\level2.rdlevel"));//Console.ReadLine() ?? string.Empty));


//Console.WriteLine();
//RDLevel l = RDLevel.LoadFile(new FileInfo("E:\\Download\\XO-XN One Forgotten Night.rdzip"));
//var a = l.Count;
//Edega.DetectingAll(l);

//CallCustomMethod callCustomMethod = new();

//var s = RhythmBase.Expressions.ExpressionTree.GetExpressionTree("atLeastNPerfects(i1,f2*3.-.5)");
//s = s;

Variables v = new();
while (true)
{
    Exp a = new($"{{{Console.ReadLine()}}}");
    var s = a.GetExpValue(v);
    Console.WriteLine($">>> {s}");
}

//l.SaveFile(new FileInfo("I:\\RhythmDoctor\\你我皆相连2\\Connected\\level2.rdlevel"));