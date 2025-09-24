
public class KeyLibrary
{
    public string[] UpKeys { get; private set; } = new string[]
    {
        "f4","f5","f6","f7","f8",
        "5","6","7","8","9",
        "r","t","y","u","i",
        "up"
    };

    public string[] BackKeys { get; private set; } = new string[]
    {
        "f","g","h","j","k",
        "v","b","n","m",
        "space",
        "down"
    };
    
    public string[] RightKeys { get; private set; } = new string[]
    {
        "f9","f10","f11","f12",
        "0","=","~","|","backspace",
        "o","p","@","{","enter",
        "l",";","+","}","'",
        "/","right shift","right alt","right ctrl",
        "right"
    };
    
    public string[] LeftKeys { get; private set; } = new string[]
    {
        "escape","f1","f2","f3",
        "1","2","3","4",
        "tab","q","w","e",
        "a","s","d",
        "left shift","z","x","c",
        "left ctrl","left alt",
        "left"
    };
    
}
