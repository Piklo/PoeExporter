using PoeData;

var standalone = new DataLoader("E:\\Programs\\Grinding Gear Games\\Path of Exile");
// var steam = new DataLoader("E:\\Programs\\Steam\\steamapps\\common\\Path of Exile");

var asd = standalone.GetFileBytes("data/acts.dat64");
Console.WriteLine();
