using Physicians;
using RhythmBase.Events;
using RhythmBase.LevelElements;

//level.SaveFile(new FileInfo("E:\\Resources\\你我皆相连2\\Connected\\level2.rdlevel"));//Console.ReadLine() ?? string.Empty));


Console.WriteLine();
RDLevel l = RDLevel.LoadFile(new FileInfo(@"O:\RhythmDoctor\你我皆相连2\Connected\level.rdlevel"));
//var a = l.Count;
//Edega.DetectingAll(l);
l.set_EventsAt(24, l.get_EventsAt(24));
//l.RemoveAll(i => i.BeatOnly == 22);
foreach (BaseEvent e in l.Where<BaseBeat>(12,49))
{
    Console.WriteLine(e.ToString());
}

//var s = RhythmBase.Expressions.ExpressionTree.GetExpressionTree("atLeastNPerfects(i1,f2*3.-.5)");
//s = s;

//Variables v = new();
//while (true)
//{
//    Exp a = new($"{{{Console.ReadLine()}}}");
//    var s = a.GetExpValue(v);
//    Console.WriteLine($">>> {s}");
//}

//l.SaveFile(new FileInfo("I:\\RhythmDoctor\\你我皆相连2\\Connected\\level2.rdlevel"));