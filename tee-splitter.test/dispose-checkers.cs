namespace tee_splitter.test;

// If I could I would derive this from the null TextWriter.
public class WriterDisposeChecker : StringWriter {
    public bool has_been_disposed { get; set; }

    protected override void Dispose(bool disposing) {
        // No need to call super.dispose since StringWriter?
        this.has_been_disposed = disposing;
    }
}
