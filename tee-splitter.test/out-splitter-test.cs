namespace tee_splitter.test;

public class OutSplitterTest
{
    [Fact]
    public void basic_char_multiwrite()
    {
        const string test_str = "My hovercraft is full of eels. huegh";
        var string_writers = new List<StringWriter>();
        for (int i = 0; i < 10; i++) {
            string_writers.Add(new StringWriter());
        }
        var generic_writers = new List<TextWriter>();
        foreach (var s in string_writers) {
            generic_writers.Add(s);
        }

        var splitter = new TextOutSplitter(generic_writers);
        for (int i = 0; i < test_str.Length; i++) {
            splitter.Write(test_str[i]);
        }

        foreach (var s in string_writers) {
            Assert.Equal(test_str, s.ToString());
        }
    }

    [Fact]
    public void out_splitter_disposes_of_all_writers() {
        var dispose_writers = new List<WriterDisposeChecker>();
        for (int i = 0; i < 10; i++) {
            dispose_writers.Add(new WriterDisposeChecker());
        }
        var writers = new List<TextWriter>();
        foreach (var w in dispose_writers) {
            writers.Add(w);
        }
        var splitter = new TextOutSplitter(writers);
        splitter.Dispose();
        foreach (var w in dispose_writers) {
            Assert.True(w.has_been_disposed);
        }
    }
}
