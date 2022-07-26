
public class ArgsParser {
    public readonly IReadOnlyList<String> files;
    public readonly bool append;
    public readonly bool ignore_signals;

    bool dash_dash, append_, ignore_signals_;
    List<String> files_ = new List<String>();

    public ArgsParser(IReadOnlyList<String> args) {
        foreach (var arg in args) {
            if (!dash_dash) {
                this.check_switches(arg);
            } else {
                this.files_.Add(arg);
            }
        }

        this.files = this.files_;
        this.append = this.append_;
        this.ignore_signals = this.ignore_signals_;
    }

    void check_switches(string arg) {
        if (arg == "--") {
            this.dash_dash = true;
        } else if (arg[0] == '-') {
            for (var i = 0; i < arg.Length; i++) {
                var c = arg[i];
                if (c == 'a')
                    this.append_ = true;
                else if (c == 'i')
                    this.ignore_signals_ = true;
                else
                    throw new InvalidArgument(c);
                }
        } else {
            files_.Add(arg);
        }
    }
}

public class InvalidArgument: Exception {
    public readonly string wrong_argument;
    
    public InvalidArgument(char c) {
        this.wrong_argument = $"{c} is not a valid argument switch.";
        Console.Error.WriteLine(wrong_argument);
    }
}
