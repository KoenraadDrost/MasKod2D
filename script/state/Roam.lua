print("Roam Script print")
-- return "Roam Script";

-- defines a factorial function
function fact (n)
    if (n == 0) then
        return 1
    else
        return n*fact(n - 1)
    end
end

-- External script test: use absolute filepath, otherwise it will look in debug build folder.(place of execution)
print("Patroling Script print: ")
print(dofile("D:/My Documents/Studie/Jaar 4/Semester 2 Gamesprogramming/Algorythms and Artificial Intelligence/PairProject/MasKod2D/script/state/Patroling.lua"))

return fact(5)