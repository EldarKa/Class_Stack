using System.ComponentModel;

namespace Class_Stack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //STACK через список ЗДОРОВОГО ЧЕЛОВЕКА!!!

            var s = new Stack("a", "b", "c");
            // size = 3, Top = 'c'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
            var deleted = s.Pop();
            // Извлек верхний элемент 'c' Size = 2
            Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
            s.Add("d");
            // size = 3, Top = 'd'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
            s.Pop();
            s.Pop();
            s.Pop();
            // size = 0, Top = null
            Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");
            s.Pop();
            var s0 = new Stack("a", "b", "c");
            s0.Merge(new Stack("1", "2", "3"));
            Console.WriteLine($"size = {s0.Size}, Top = '{s0.Top}'");

            Console.WriteLine($"-----------------------------------------------");

            //STACK1 РЕКУРСИВНОГО ЧЕЛОВЕКА, не вкусно НО метод Merge с выкладыванием перекладыванием забавный

            var s1 = new Stack1("a", "b", "c");
            // size = 3, Top = 'c'
            Console.WriteLine($"size = {s1.Size}, Top = '{s1.Top}'");
            var deleted1 = s1.Pop();
            // Извлек верхний элемент 'c' Size = 2
            Console.WriteLine($"Извлек верхний элемент '{deleted1}' Size = {s1.Size}");
            s1.Add("d");
            // size = 3, Top = 'd'
            Console.WriteLine($"size = {s1.Size}, Top = '{s1.Top}'");
            s1.Pop();
            s1.Pop();
            s1.Pop();
            // size = 0, Top = null
            Console.WriteLine($"size = {s1.Size}, Top = {(s1.Top == null ? "null" : s1.Top)}");
            s1.Pop();
            var s01 = new Stack("a", "b", "c");
            s01.Merge(new Stack("1", "2", "3"));
            Console.WriteLine($"size = {s01.Size}, Top = '{s01.Top}'");
        }
    }
}

public class Stack
{
    public List<string> mylist { get; set; }

    public int Size { get => mylist.Count; }
    public object Top { get => Size != 0 ? mylist.Last() : null; }

    public Stack(params string[] ls)
    {
        this.mylist = new List<string>(ls);
    }

    internal void Add(string v)
    {
        mylist.Add(v);
    }

    public object Pop()
    {
        if (Top == null) { return null; }
        string s = Top.ToString();
        mylist.RemoveAt(Size - 1);
        return s;
    }

    public static Stack Concat(params Stack[] stacks)
    {
        Stack s = new Stack();
        foreach (var s1 in stacks) {s.Merge(s1);}
        return s;
    }
}

public static class StackExtensions
{
    public static void Merge(this Stack s1, Stack s2)
    {

        while (s2.Size > 0)
        {
            s1.Add(s2.Pop().ToString());
        }
    }
}

public class Stack1
{
    private class StackItem
    {
        public string V { get; }
        public StackItem Last_V { get; }
        public StackItem(string v, StackItem lv)
        {
            V = v;
            Last_V = lv;
        }
    }
    private StackItem last_SI; // Верхний элемент стека
    private int size; // долго думал стоит ли заводить этот параметр или считать его динамически
    //до пустого значения  last_SI если потребуетсянапишуподобнуюреализацию

    public int Size { get => size; }
    public object Top { get => size != 0 ? last_SI.V : null; }

    public Stack1(params string[] array)
    {
        foreach (var v in array)
        {
            Add(v);
        }
    }

    internal void Add(string v)
    {
        last_SI = new StackItem(v, last_SI);
        size++;
    }

    public object Pop()
    {
        if (last_SI == null) { return null; }
        string v = last_SI.V;
        last_SI = last_SI.Last_V;
        size--;
        return v;
    }

    public static Stack1 Concat(params Stack1[] stacks)
    {
        Stack1 s = new Stack1();
        foreach (var s1 in stacks) { s.Merge(s1); }
        return s;
    }
}

public static class Stack1Extensions
{
    public static void Merge(this Stack1 s1, Stack1 s2)
    {
        var tempStack = new Stack1();
        while (s2.Size > 0)
        {
            tempStack.Add(s2.Pop().ToString());
        }

        while (tempStack.Size > 0)
        {
            s1.Add(tempStack.Pop().ToString());
        }
    }
}