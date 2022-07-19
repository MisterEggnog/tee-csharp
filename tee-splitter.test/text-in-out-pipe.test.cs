namespace tee_splitter.test;

public class TextInOutPipeTest {
    [Fact]
    public void transfers_data() {
        const string test_string = "This is the test string.\nYou have reached test string.";
        var input = new StringReader(test_string);
        var output = new StringWriter();
        var pipe = new TextTransferPipe(input, output);
        pipe.transfer();
        Assert.Equal(test_string, output.ToString());
    }

    [Fact]
    public void text_in_out_disposes_properly() {
        var dispose_out = new tee_splitter.test.WriterDisposeChecker();
        var dispose_in = new tee_splitter.test.ReaderDisposeChecker();
        IDisposable dispose_pipe = new TextTransferPipe(dispose_in, dispose_out);
        dispose_pipe.Dispose();
        Assert.True(dispose_in.has_been_disposed);
        Assert.True(dispose_out.has_been_disposed);
    }
}
