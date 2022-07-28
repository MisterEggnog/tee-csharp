
public class ArgsParser {
    public readonly IReadOnlyList<String> files;
    public readonly bool append;
    public readonly bool ignore_signals;
    public readonly bool print_version_info;
    public readonly bool print_help_info;
    public readonly bool print_license_info;

    bool dash_dash, append_, ignore_signals_, version_info, help_info, license_info;
    List<String> files_ = new List<String>();

    public ArgsParser(IReadOnlyList<String> args) {
        foreach (var arg in args) {
            if (!dash_dash) {
                this.check_switches(arg);
            } else {
                this.files_.Add(arg);
            }
        }

        if (this.version_info || this.help_info || this.license_info) {
            this.files = new List<String>();
            this.print_version_info = this.version_info;
            this.print_help_info = this.help_info;
            this.print_license_info = this.license_info;
        } else {
            this.files = this.files_;
            this.append = this.append_;
            this.ignore_signals = this.ignore_signals_;
        }
    }

    void check_switches(string arg) {
        if (arg == "--") {
            this.dash_dash = true;
        } else if (arg == "--version") {
            this.version_info = true;
        } else if (arg == "--help") {
            this.help_info = true;
        } else if (arg == "--license") {
            this.license_info = true;
        } else if (arg[0] == '-') {
            for (var i = 1; i < arg.Length; i++) {
                var c = arg[i];
                this.check_short_switches(c, i);
            }
        } else {
            files_.Add(arg);
        }
    }

    public void check_short_switches(char c, int str_pos) {
        switch (c) {
            case 'a':
                this.append_ = true;
                break;
            case 'i':
                this.ignore_signals_ = true;
                Console.Error.WriteLine("tee does not actually ignore signals");
                break;
            case 'v':
                this.version_info = true;
                break;
            case 'h':
                this.help_info = true;
                break;
            default:
                throw new FormatException($"{c} (pos {str_pos}) is not a valid argument switch.");
        }
    }
}
