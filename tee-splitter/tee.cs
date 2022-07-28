
public class Tee {
    ArgsParser args;

    public Tee(ArgsParser args) {
        this.args = args;
    }

    public Tee(IReadOnlyCollection<String> files) {
        var as_array = new List<String>(files);
        this.args = new ArgsParser(as_array);
    }

    List<TextWriter> open_files(IReadOnlyCollection<String> files) {
        var open_files = new List<TextWriter>();
        foreach (var s in files) {
            open_files.Add(new StreamWriter(s, false, System.Text.Encoding.UTF8));
        }
        return open_files;
    }

    public int run() {
        // Why isn't there no disposable ReadOnlyCollection?
        var open_files = this.open_files(args.files);
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
