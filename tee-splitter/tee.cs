
public class Tee {
    ArgsParser args;

    // TODO Write actual help string
    const String help_message = "aaaa";

    public Tee(ArgsParser args) {
        this.args = args;
    }

    public Tee(IReadOnlyCollection<String> files) {
        var as_array = new List<String>(files);
        this.args = new ArgsParser(as_array);
    }

    List<TextWriter> open_files(IReadOnlyCollection<String> files, bool append) {
        var open_files = new List<TextWriter>();
        foreach (var s in files) {
            open_files.Add(new StreamWriter(s, append, System.Text.Encoding.UTF8));
        }
        return open_files;
    }

    public int run() {
        if (this.args.print_help_info) {
            Console.WriteLine(help_message);
            return 0;
        }
        if (this.args.print_version_info) {
            var meta = new ProgramMetadata();
            Console.WriteLine(meta.version_str());
            return 0;
        }
        if (this.args.print_license_info) {
            var meta = new ProgramMetadata();
            Console.WriteLine(meta.license_str());
            return 0;
        }

        // Why isn't there no disposable ReadOnlyCollection?
        var open_files = this.open_files(args.files, this.args.append);
        try {
            open_files.Add(Console.Out);
            using (var output_splitter = new TextOutSplitter(open_files)) {
                var pipe = new TextTransferPipe(Console.In, output_splitter);
                pipe.transfer();
            }
        } finally {
            foreach (var f in open_files)
                f.Dispose();
        }

        return 0;
    }
}
