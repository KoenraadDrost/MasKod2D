print("patroling script Script print")
-- return "Roam Script";
-- defines a root function
function root (n)
    for i = 2, n/2 do
        if(i * i == n) then return i
        end
    end
end

-- print(tostring(root(81)))

return root(81)