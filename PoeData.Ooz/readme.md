```
git clone --recursive https://github.com/zao/ooz.git
```

for linux

```
cmake -B build-ooz-release -S ooz -DOOZ_BUILD_BUN=OFF -DOOZ_BUILD_EXE=OFF -DCMAKE_BUILD_TYPE=Release
cmake --build build-ooz-release/
```

for windows

```
cmake -B build-ooz-release -S ooz -DOOZ_BUILD_BUN=OFF -DOOZ_BUILD_EXE=OFF
cmake --build build-ooz-release/ --config Release
```