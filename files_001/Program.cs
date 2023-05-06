class ListPair<T>
{
    internal List<T>? List { get; set; }
    internal List<T>? Sequence { get; set; }

    internal ListPair(List<T>? list = null, List<T>? seq = null)
    {
        List = list;
        Sequence = seq;
    }

    internal bool CheckSubsequence()
    {
        if (List == null || Sequence == null || List.Count == 0 || Sequence.Count == 0)
        {
            return false;
        }

        int listIt = 0;
        int seqIt = 0;

        while (listIt < List.Count && seqIt < Sequence.Count)
        {
            #pragma warning disable
            if (List[listIt].Equals(Sequence[seqIt]))
            {
                seqIt++;
            }
            listIt++;
        }

        return seqIt == Sequence.Count;
    }

    public override string ToString()
    {
        string res = "\"list\": ";

        if (List == null)
        {
            res += "null";
        }
        else
        {
            res += "[";

            for (int i = 0; i < List.Count; i++)
            {
                #pragma warning disable
                res += List[i].ToString();

                if (i < List.Count - 1)
                    res += ", ";
            }

            res += "]\n\"seq\": ";
        }

        if (Sequence == null)
        {
            res += "null";
        }
        else
        {
            res += "[";

            for (int i = 0; i < Sequence.Count; i++)
            {
                #pragma warning disable
                res += Sequence[i].ToString();

                if (i < Sequence.Count - 1)
                    res += ", ";
            }

            res += $"]\nis subsequence => {CheckSubsequence()}\n";
        }

        return res;
    }
}

class Program
{
    static void Main()
    {
        // вхідний файл (файл з початковими даними) - "data.txt"
        // вихідний файл (файл з результатом) - "solution.txt"
        // всі файли вже створені в проекті, ви зможете їх знайти в 'Solution Explorer'

        List<ListPair<int>> allListPairs = new();
        string readFilePath = "..\\..\\..\\data.txt";
        string writeFilePath = "..\\..\\..\\solution.txt";
        string forRead = string.Empty;
        string forWrite = string.Empty;

        int count = 0;
        ConsoleKeyInfo keyInfo = new();

        try
        {
            using (StreamReader sr = new(readFilePath))
            {
                while (!string.IsNullOrEmpty(forRead = new string(sr.ReadLine())))
                {
                    if (allListPairs.Count == 0 || allListPairs[allListPairs.Count - 1].Sequence != null)
                    {
                        allListPairs.Add(new ListPair<int>(forRead.Split(',').Select(int.Parse).ToList()));
                    }
                    else
                    {
                        allListPairs[allListPairs.Count - 1].Sequence = forRead.Split(',').Select(int.Parse).ToList();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }

        foreach (var listPair in allListPairs)
        {
            forWrite += $"{{{++count}}}\n";
            forWrite += listPair + "\n";
        }

        do
        {
            Console.Clear();
            Console.WriteLine($"choose output way:\n\n[1] -> console\n[2] -> local program file ({writeFilePath})");
            keyInfo = Console.ReadKey();
        }
        while (keyInfo.KeyChar != '1' && keyInfo.KeyChar != '2');

        Console.Clear();

        switch (keyInfo.KeyChar)
        {
            case '1':

                Console.Write(forWrite);
                break;

            case '2':

                try
                {
                    using (StreamWriter sw = new(writeFilePath))
                    {
                        sw.Write(forWrite);
                    }

                    Console.Write("done!");
                }
                catch(Exception ex)
                {
                    Console.Write(ex);
                }

                break;
        }
    }
}