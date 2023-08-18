using System;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyChaoLoveStory;

ImmutableDictionary<string, string>? mapping = JsonSerializer.Deserialize(DefaultDataSource.DEFAULT_ARKNIGHTS_OPERATORS,
                                                                          ImmutableDictionaryStrStrSourceGenerationContext.Default.ImmutableDictionaryStringString);

if (mapping is null)
{
    Console.WriteLine("原始数据为空，无法继续执行程序！");
    return;
}

const string DOCTOR = "博士";

List<string> temp = new(mapping.Values)
{
    DOCTOR
};
string[] people = temp.ToArray();

string[] template = DefaultDataSource.DEFAULT_ARKNIGHTS_TEMPLATE;

while (true)
{
    Random random = new();
    bool isDoctor;

    Console.WriteLine("My chao love story");
    Console.WriteLine("By Baka632");
    Console.WriteLine("GitHub: https://github.com/Baka632");
    Console.WriteLine("仅供图君一笑，不必照单全收 :)");
    Console.WriteLine();
    Console.WriteLine("选择模式");
    Console.WriteLine("(1) 博士");
    Console.WriteLine("(2) 随机干员");

    ConsoleKeyInfo key = Console.ReadKey(true);

    if (key.Key is ConsoleKey.D1)
    {
        isDoctor = true;
    }
    else if (key.Key is ConsoleKey.D2)
    {
        isDoctor = false;
    }
    else if (key.Key == ConsoleKey.Escape)
    {
        break;
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("输入无效，按 Esc 退出，按其它键重试");

        ConsoleKeyInfo consoleKey = Console.ReadKey(true);

        if (consoleKey.Key == ConsoleKey.Escape)
        {
            break;
        }

        Console.Clear();
        continue;
    }

    while (true)
    {
        List<string> result = new(template.Length);

        int mainCharactorIndex;
        if (isDoctor)
        {
            mainCharactorIndex = Array.IndexOf(people, DOCTOR);
        }
        else
        {
            mainCharactorIndex = random.Next(people.Length);
        }

        string mainCharactor = people[mainCharactorIndex];

        Console.Clear();
        Console.WriteLine($"{mainCharactor}的混乱生活");
        Console.WriteLine();

        for (int i = 0; i < template.Length;)
        {
            int index = random.Next(people.Length);

            if (index == mainCharactorIndex)
            {
                continue;
            }

            string selectedPerson = people[index];

            result.Add($"{template[i]}{selectedPerson}");
            i++;
        }

        foreach (string value in result)
        {
            Console.WriteLine(value);
        }

        Console.WriteLine();
        Console.WriteLine("按任意键继续，按 Esc 返回到上一菜单");

        if (Console.ReadKey().Key == ConsoleKey.Escape)
        {
            Console.Clear();
            break;
        }
        else
        {
            Console.Clear();
            continue;
        }
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ImmutableDictionary<string, string>))]
internal partial class ImmutableDictionaryStrStrSourceGenerationContext : JsonSerializerContext
{
}