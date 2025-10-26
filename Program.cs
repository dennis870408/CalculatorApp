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
            result = a + b;
            break;
        case '-':
            result = a - b;
            break;
        case '*':
            result = a * b;
            break;
        case '/':
            try
            { 
                result = a / b;
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