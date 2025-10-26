// See https://aka.ms/new-console-template for more information
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
        if (b != 0)
        {
            result = a / b;
        }
        else
        {
            Console.WriteLine("錯誤: 除數不能為零");
            return;
        }
        break;
    default:
        Console.WriteLine("錯誤: 無效的運算方式");
        return;
}

Console.WriteLine("結果是: " + result);
