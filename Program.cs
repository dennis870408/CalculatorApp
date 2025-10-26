// See https://aka.ms/new-console-template for more information


while (true)
{
    int result = SimpleCalculator();
    Console.WriteLine("計算結果為: " + result);
    char cont;
    Console.WriteLine("是否繼續計算? (y/n):");
    cont = Convert.ToChar(Console.ReadLine());
    if (cont == 'n' || cont == 'N')
    {
        break;
    }
}

int Add(int x, int y)
{
    return x + y;
}

int Subtract(int x, int y)
{
    return x - y;
}

int Multiply(int x, int y)
{
    return x * y;
}

int Divide(int x, int y)
{
    return x / y;
}
int SimpleCalculator()
{
    Console.WriteLine("輸入第一個數字:");
    int a;
    a = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("輸入第二個數字:");
    int b;
    b = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("選擇運算方式:");
    char op;
    op = Convert.ToChar(Console.ReadLine());

    int result = 0;

    switch (op)
    {
        case '+':
            result = Add(a, b);
            break;
        case '-':
            result = Subtract(a, b);
            break;
        case '*':
            result = Multiply(a, b);
            break;
        case '/':
            try
            { 
                result = Divide(a, b);
            }
            catch (DivideByZeroException)
            {
            Console.WriteLine("錯誤: 除以零");
            return 0;
            }
            break;
        default:
            Console.WriteLine("錯誤: 無效的運算符");
            return 0;

    }

    return result;
}