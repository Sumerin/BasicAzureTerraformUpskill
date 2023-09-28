# BasicAzureTerraformUpskill

---
 
## Upskill partitioner info

### How to start

#### .NET
* Add libraries from DLL folder to your project
* Add PdfSharp 6.0.0-preview-3
* Run

See examples project for details


### Access Keys
  
#### Risk Assessment


>DevKey = "BDD3CBE2-4692-4E74-BF19-79444229343B";  
>ProdKey = "506333AD-F056-4085-9FDC-06A9D87D3683";
  
#### Rodo Assessment

>DevKey = "E15EF5C8-5172-418D-9A2E-E04C300C34D9";  
>ProdKey = "84052DC8-0D90-4DA8-B007-16AA227E908F";


--- 

## Contribution Details

### Tags
To simplify things all the details returned by library are stored in **Keywords/Tags** section. 
PdfSharp is only used to extract keywords from PDF.
Keywords are in format

``
<key>=<value>;<key2>=<value2>...
``
 
The pdf size have been tweaked using 
https://pdf.pi7.org/increase-pdf-size
which adds null bytes to the end of file.     


### Build libraries
     
#### .NET

> dotnet publish ./Net/ -o ./Net/DLL

### Third-Party Libraries
* https://github.com/empira/PDFsharp
