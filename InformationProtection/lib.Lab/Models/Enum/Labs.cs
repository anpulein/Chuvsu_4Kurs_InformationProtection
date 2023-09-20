using System.ComponentModel;

namespace lib.Lab.Models.Enum;

public enum Labs
{
    [Description("Шифрование данных методом подстановки (1)")]
    Lab1,
    [Description("Шифрование данных методом перестановки (2)")]
    Lab2,
    [Description("Линейное шифрование данных (Гаммирование) (3)")]
    Lab3,
    [Description("Класический криптографический алгоритм DESC (4)")]
    Lab4,
    [Description("Работа алгоритма DES в режиме CBC (5)")]
    Lab5,
    [Description("Работа алгоритма DES в режиме CFB (6)")]
    Lab6,
    [Description("Работа алгоритма DES в режиме OFB (7)")]
    Lab7,
    [Description("Метод изменения интервала между предложениями (8)")]
    Lab8,
    [Description("Метод изменения кол-ва пробелов в конце текстовых строк (9)")]
    Lab9
}