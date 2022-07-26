namespace tee_splitter.test;

// If I could I would derive these from the null Writers/Readers.

public class WriterDisposeChecker : StringWriter {
    public bool has_been_disposed { get; set; }

    protected override void Dispose(bool disposing) {
        // No need to call super.dispose since StringWriter?
        this.has_been_disposed = disposing;
    }

    [Fact]
    public void dispose_checker_sets_bool() {
        var dispose_checker = new WriterDisposeChecker();
        Assert.False(dispose_checker.has_been_disposed);
        dispose_checker.Dispose();
        Assert.True(dispose_checker.has_been_disposed);
    }
}

public class ReaderDisposeChecker: TextReader {
    public bool has_been_disposed { get; set; }

    protected override void Dispose(bool disposing) {
        this.has_been_disposed = disposing;
    }

    [Fact]
    public void dispose_checker_sets_bool() {
        var dispose_checker = new ReaderDisposeChecker();
        Assert.False(dispose_checker.has_been_disposed);
        dispose_checker.Dispose();
        Assert.True(dispose_checker.has_been_disposed);
    }
}
