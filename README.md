# Image converter 
_imgconv_ is a small project to convert images to .svg format and it's serving API to do it.


## Installation

Use the [docker run](https://docs.docker.com/engine/reference/run/) command to create container.

```bash
docker run -d -p 5000:80 edica/imgconv
```

## Usage

GET (will receive all files on server):
```
http://localhost:5000/api/file
```
RESULT
```
[
    "http://localhost:5000/test.png",
    "http://localhost:5000/c60741ec-4dea-4594-8088-30344a712b50.png",
    "http://localhost:5000/cde791a5-bba7-46b9-be34-33be931131ca.png",
    "http://localhost:5000/df9d6a45-f44a-4c6f-a728-6e391a561b5e.png"
]
```

POST (need to be form-data and has formfiles):
```
http://localhost:5000/api/file
```
RESULT
```
{
    "count": 1,
    "size": 245,
    "filePath": "/app/wwwroot",
    "files": [
        "http://localhost:5000/36c0805d-d060-40f6-a1c0-72162fe49416.svg"
    ]
}
```

## License

Licensed under the ImageMagick License (the "License"); you may not use
this file except in compliance with the License.  You may obtain a copy
of the License at

    https://imagemagick.org/script/license.php

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the
License for the specific language governing permissions and limitations
under the License.

[GNU](https://www.gnu.org/licenses/gpl.html)

