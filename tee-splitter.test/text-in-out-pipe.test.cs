namespace text_in_out_pipe.test;

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
        var dispose_out = new tee_splitter.test.TextWriterDisposeChecker();
    }
}
