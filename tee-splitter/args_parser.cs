
public class ArgsParser {
    public readonly IReadOnlyList<String> files;
    public readonly bool append;
    public readonly bool ignore_signals;
    public readonly bool print_version_info;
    public readonly bool print_help_info;

    bool dash_dash, append_, ignore_signals_, version_info, help_info;
    List<String> files_ = new List<String>();

    public ArgsParser(IReadOnlyList<String> args) {
        foreach (var arg in args) {
            if (!dash_dash) {
                this.check_switches(arg);
            } else {
                this.files_.Add(arg);
            }
        }

        if (!this.version_info) {
            this.files = this.files_;
            this.append = this.append_;
            this.ignore_signals = this.ignore_signals_;
        } else {
            this.files = new List<String>();
            this.print_version_info = this.version_info;
        }
    }

    void check_switches(string arg) {
        if (arg == "--") {
            this.dash_dash = true;
        } else if (arg == "--version") {
            this.version_info = true;
        } else if (arg[0] == '-') {
            for (var i = 1; i < arg.Length; i++) {
                var c = arg[i];
                if (c == 'a')
                    this.append_ = true;
                else if (c == 'i') {
                    this.ignore_signals_ = true;
                    Console.Error.WriteLine("tee does not actually ignore signals");
                } else if (c == 'v')
                    this.version_info = true;
                else
                    throw new FormatException($"{c} (pos {i}) is not a valid argument switch.");
                }
        } else {
            files_.Add(arg);
        }
    }
}
