// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;

public class Program
{
    public static void Main(string[]args){

        ICopy copyFile=new CopyFile("app.exe","app-copy.exe");
       copyFile.Copy();

        IInverted file=new InvertedFile("menu.csv","menu.txt");
        file.Inverted();

        EncryptBmpImage image=new EncryptBmpImage("logo.bmp");
        image.Encrypt();
        
    }
}



public interface ICopy
{
    void Copy();
}

public interface IInverted
{
    void Inverted();
}

public class CopyFile:ICopy
{

    public string InputFileName{get;set;}

    public string OutputFileName{get;set;}


    public CopyFile(string inputFileName,string outputFileName)
    {
        InputFileName=inputFileName;
        OutputFileName=outputFileName;

    }
    public void Copy()
    {
        const int BUFFER_SIZE= 521 * 1024;
        byte []data =new byte[BUFFER_SIZE];


        //copia del file
        using (FileStream inputFile=File.OpenRead(InputFileName)){
            using (FileStream outputFile=File.Create( OutputFileName)){
                int amountRead;
                do{
                    amountRead=inputFile.Read(data,0,BUFFER_SIZE);
                    outputFile.Write(data,0,amountRead);

                    
                }while(amountRead ==BUFFER_SIZE);
            }
        }

        
       

    }

}


public class InvertedFile:IInverted
{
    public string InputFileName{get;set;}

    public string OutputFileName{get;set;}


    public InvertedFile(string inputFile,string outputFile)
    {
        InputFileName=inputFile;
        OutputFileName=outputFile;

    }

    public void Inverted()
    {
        using (FileStream file=File.OpenRead(InputFileName))
        {
            long size=file.Length;
            byte[]data=new byte[size];

            file.Read(data,0,(int)size);

            using (FileStream outFile=File.Create(OutputFileName)){
                for(long i =size -1;i>0;i--){
                    outFile.WriteByte(data[i]);
                }
            }


        }

       

    }
}

//scaricata estensione VERIV  da VSCode 
public class EncryptBmpImage
{
    public string FileImage{get;set;}

    public EncryptBmpImage(string fileImage){
        FileImage=fileImage;
    }

    public void Encrypt(){
        using (FileStream file=File.Open(FileImage,FileMode.Open,FileAccess.ReadWrite)){
            
            char b1=(char)file.ReadByte();
            char b2=(char)file.ReadByte();

            if (b1 !='B'||b2  !='M'){

                return; // Not a BMP file

            }
            else
            {
                file.Seek(0,SeekOrigin.Begin);
                file.WriteByte((byte)'M');
                file.WriteByte((byte)'B');


            }

        }

    }


}


