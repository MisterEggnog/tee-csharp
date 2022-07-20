
public class Tee {

    static List<TextWriter> open_files(IReadOnlyCollection<String> files) {
        // TODO
        return new List<TextWriter>();
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
