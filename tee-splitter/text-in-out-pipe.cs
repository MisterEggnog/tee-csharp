
public class TextTransferPipe
{
    private TextReader input;
    private TextWriter output;

    public TextTransferPipe(TextReader input, TextWriter output)
    {
        this.input = input;
        this.output = output;
    }

    public void transfer()
    {
        throw new NotImplementedException();
    }
}
