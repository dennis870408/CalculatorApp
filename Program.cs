using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("輸入一個算式 (例如 5*8+4 或 3+4*2/(1-5)): ");
            string s = Console.ReadLine();

            try
            {
                double result = SimpleCalculator(s);
                Console.WriteLine($"計算結果為: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 錯誤: {ex.Message}");
            }

            Console.Write("是否繼續計算? (y/n): ");
            string cont = Console.ReadLine();
            if (!string.IsNullOrEmpty(cont) && (cont[0] == 'n' || cont[0] == 'N'))
                break;

            Console.WriteLine();
        }
    }

    // ===== 這裡以下是計算邏輯 =====

    static double SimpleCalculator(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            throw new ArgumentException("輸入為空。");

        s = s.Replace(" ", "");

        var tokens = Tokenize(s);
        var rpn = ToRPN(tokens);
        return EvaluateRPN(rpn);
    }

    static List<string> Tokenize(string s)
    {
        var tokens = new List<string>();
        int i = 0;
        while (i < s.Length)
        {
            char c = s[i];

            // 處理數字（包含小數點）
            if (char.IsDigit(c) || c == '.')
            {
                int start = i;
                while (i < s.Length && (char.IsDigit(s[i]) || s[i] == '.')) i++;
                tokens.Add(s.Substring(start, i - start));
                continue;
            }

            // 處理運算子與括號
            if ("+-*/()".IndexOf(c) >= 0)
            {
                // 處理負號當作一元運算（如 -3）
                if (c == '-')
                {
                    bool isUnary = (tokens.Count == 0) ||
                                   (tokens[tokens.Count - 1] == "(") ||
                                   ("+-*/".IndexOf(tokens[tokens.Count - 1]) >= 0);
                    if (isUnary)
                    {
                        tokens.Add("0");
                    }
                }

                tokens.Add(c.ToString());
                i++;
                continue;
            }

            throw new FormatException($"包含不支援的字元: '{c}'");
        }

        return tokens;
    }

    static List<string> ToRPN(List<string> tokens)
    {
        var output = new List<string>();
        var ops = new Stack<string>();

        int Prec(string op)
        {
            return op switch
            {
                "+" or "-" => 1,
                "*" or "/" => 2,
                _ => 0
            };
        }

        foreach (var t in tokens)
        {
            if (double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            {
                output.Add(t);
            }
            else if (t == "+" || t == "-" || t == "*" || t == "/")
            {
                while (ops.Count > 0)
                {
                    var top = ops.Peek();
                    if (top == "(") break;
                    if (Prec(top) >= Prec(t))
                        output.Add(ops.Pop());
                    else
                        break;
                }
                ops.Push(t);
            }
            else if (t == "(")
            {
                ops.Push(t);
            }
            else if (t == ")")
            {
                bool foundLeft = false;
                while (ops.Count > 0)
                {
                    var top = ops.Pop();
                    if (top == "(")
                    {
                        foundLeft = true;
                        break;
                    }
                    output.Add(top);
                }
                if (!foundLeft) throw new FormatException("括號不配對");
            }
            else
            {
                throw new FormatException($"不支援的 token: {t}");
            }
        }

        while (ops.Count > 0)
        {
            var top = ops.Pop();
            if (top == "(" || top == ")") throw new FormatException("括號不配對");
            output.Add(top);
        }

        return output;
    }

    static double EvaluateRPN(List<string> rpn)
    {
        var st = new Stack<double>();
        foreach (var t in rpn)
        {
            if (double.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out double num))
            {
                st.Push(num);
            }
            else
            {
                if (st.Count < 2) throw new FormatException("運算式格式錯誤");
                double b = st.Pop();
                double a = st.Pop();
                double res = t switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => b == 0 ? throw new DivideByZeroException("除數不能為 0") : a / b,
                    _ => throw new FormatException($"不支援運算子: {t}")
                };
                st.Push(res);
            }
        }
        if (st.Count != 1) throw new FormatException("運算式格式錯誤");
        return st.Pop();
    }
}
