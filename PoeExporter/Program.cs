using PoeData;

var loader = new StandaloneLoader("E:\\Programs\\Grinding Gear Games\\Path of Exile");
var b1 = loader.ReadIndex();

var steam = new SteamLoader("E:\\Programs\\Steam\\steamapps\\common\\Path of Exile");
var b2 = steam.ReadIndex();

Console.WriteLine(b1.SequenceEqual(b2));
