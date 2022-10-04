namespace HalloDelegate
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitPara(string msg);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegate
    {
        

        public HalloDelegate()
        {
            EinfacherDelegate einfacherDelegate = EinfacheMethode;
            Action einfacheAction = EinfacheMethode;
            Action einfacheActionAno = delegate () { Console.WriteLine("Hallo"); };
            Action einfacheActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action einfacheActionAno3 = () => Console.WriteLine("Hallo");

            DelegateMitPara deleMitPara = MethodeMitPara;
            Action<string> actionMitPara = MethodeMitPara;
            Action<string> actionAno = delegate (string txt) { Console.WriteLine(txt); };
            Action<string> actionAno2 = (string txt) => { Console.WriteLine(txt); };
            Action<string> actionAno3 = (txt) => Console.WriteLine(txt);
            Action<string> actionAno4 = x => Console.WriteLine(x);

            CalcDelegate calcDele = Minus;
            Func<int, int, long> funcDele = Sum;
            Func<int, int, long> funcDeleAno = delegate (int a, int b) { return a + b; };
            Func<int, int, long> funcDeleAno2 = (int a, int b) => { return a + b; };
            Func<int, int, long> funcDeleAno3 = (a, b) => { return a + b; };
            Func<int, int, long> funcDeleAno4 = (a, b) => a + b;

            List<string> texte = new();

            texte.Where(x => x.StartsWith("b"));
            texte.Where(Filter);
        }
 

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else 
                return false;
        }

        private long Minus(int a, int b)
        {
            return a - b;
        }

        long Sum(int a, int b)
        {
            return a + b;
        }

        void MethodeMitPara(string txt)
        {
            Console.WriteLine(txt);
        }

        void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}
