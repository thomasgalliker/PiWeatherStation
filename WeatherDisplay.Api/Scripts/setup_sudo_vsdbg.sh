#!/bin/bash

mv ~/.vs-debugger/vs2022/vsdbg ~/.vs-debugger/vs2022/vsdbg-bin

touch ~/.vs-debugger/vs2022/vsdbg

cat > ~/.vs-debugger/vs2022/vsdbg <<EOF
#!/bin/bash
sudo ~/.vs-debugger/vs2022/vsdbg-bin $@
EOF

chmod +x ~/.vs-debugger/vs2022/vsdbg