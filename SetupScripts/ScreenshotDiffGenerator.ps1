param (
    [string]$dir = $(throw "-dir is required.")
)
$currentDir = $dir;
$resultDir = "$currentDir\Result"
$resultFile = "$resultDir\report.txt"

$compare = "C:\ImageMagick-6.8.9-6\compare.exe";
$images = Get-ChildItem | Where-Object { !$_.PSIsContainer } | Where-Object { $_.Name.EndsWith("-expected.jpg") }

if (Test-Path -Path $resultDir)
{
    Remove-Item $resultDir -recurse
}

New-Item -ItemType directory -Path $resultDir

foreach($image in $images)
{
    $expected = $image.FullName
    $actual = $expected.Replace("-expected.jpg", "-actual.jpg")
    $difference = $expected.Replace("-expected.jpg", "-diff.jpg")
    $res = &$compare -metric RMSE $expected $actual $difference 2>&1

    if($res.ToString() -ne "0 (0)")
    {
        $actualImg = $actual.SubString($actual.LastIndexOf("\") + 1);
        $expectedImg = $expected.SubString($expected.LastIndexOf("\") + 1);
        Add-Content $resultFile "[$actualImg vs $expectedImg]: $res"
        Copy-Item -Path $expected $resultDir
        Copy-Item -Path $actual $resultDir
        Copy-Item -Path $difference $resultDir
    }
}