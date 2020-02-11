$ENV:Path += ";C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\Roslyn"
# $ENV:Path.Split(';')
md -Force ref | Out-Null
# The -refout and -refonly options are mutually exclusive.
# https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/refout-compiler-option
# https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/refonly-compiler-option
# Reference assemblies further remove metadata (private members) from metadata-only assemblies:
# csc.exe -target:library /langversion:7.1 OriginalRecipeFriedChicken.cs -refonly -out:ref/TheColonelsSecretRecipe.dll
Remove-Item -Force ref/TheColonelsSecretRecipe.dll -ErrorAction SilentlyContinue
csc.exe -target:library /langversion:7.1 OriginalRecipeFriedChicken.cs -refout:ref/TheColonelsSecretRecipe.dll