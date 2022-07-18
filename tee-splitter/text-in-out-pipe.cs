
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
        while (this.input.Peek() != -1) {
            this.output.Write((char)this.input.Read());
        }
    }
}
