
public class ArgsParser {
    public readonly IReadOnlyList<String> files;
    public readonly bool append;
    public readonly bool ignore_signals;

    public ArgsParser(IReadOnlyList<String> args) {
        var files = new List<String>();
        bool dash_dash = false;

        foreach (var arg in args) {
            if (!dash_dash) {
                if (arg[0] == '-') {
                    foreach (var c in arg) {
                        if (c == 'a')
                            this.append = true;
                        else if (c == 'i')
                            this.ignore_signals = true;
                        else if (c == '-')
                            // Hack
                            dash_dash = true;
                        else
                            throw new InvalidArgument($"{c} is not a valid argument switch.");
                    }
                } else {
                    files.Add(arg);
                }
            } else {
                files.Add(arg);
            }
        }

        this.files = files;
    }
}

public class InvalidArgument: Exception {
    readonly string wrong_argument;
    
    public InvalidArgument(string msg) {
        this.wrong_argument = msg;
    }
}
