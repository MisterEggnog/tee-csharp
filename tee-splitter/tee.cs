
public class Tee {

    static List<TextWriter> open_files(IReadOnlyCollection<String> files) {
        var open_files = new List<TextWriter>();
        foreach (var s in files) {
            open_files.Add(new StreamWriter(s, false, System.Text.Encoding.UTF8));
        }
        return open_files;
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
