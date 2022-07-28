
public class Tee {
    ArgsParser args;

    public Tee(ArgsParser args) {
        this.args = args;
    }

    public Tee(IReadOnlyCollection<String> files) {
        var as_array = new List<String>(files);
        this.args = new ArgsParser(as_array);
    }

    static List<TextWriter> open_files(IReadOnlyCollection<String> files) {
        var open_files = new List<TextWriter>();
        foreach (var s in files) {
            open_files.Add(new StreamWriter(s, false, System.Text.Encoding.UTF8));
        }
        return open_files;
    }

    public int run() {
        throw new NotImplementedException();
    }

    public static int run(IReadOnlyCollection<String> files) {
        var open_files = Tee.open_files(files);
        open_files.Add(Console.Out);
        var output_splitter = new TextOutSplitter(open_files);
        var pipe = new TextTransferPipe(Console.In, output_splitter);
        pipe.transfer();
        pipe.Dispose();

        return 0;
    }
}
