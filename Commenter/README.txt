To remove all comments
sed 's/\/\/.*$//g'

To remove all blank comments and move all comments to be just one space apart
sed 's/ \/\/ *$//g' | sed 's/ *\/\// \/\//g'